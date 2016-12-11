package part1

import akka.actor.{Actor, ActorRef, ActorSystem, Props}
import akka.pattern.ask
import akka.util.Timeout

import scala.collection.mutable
import scala.collection.mutable.ListBuffer
import scala.concurrent.Await
import scala.concurrent.duration._

import scala.util.parsing.combinator.JavaTokenParsers

trait DomainDef {

  val system = ActorSystem("part1")
  implicit val timeout = Timeout(5 seconds) // needed for akka ask pattern

  type Actions = List[Action]
  type Bots = mutable.Map[Int, ActorRef]
  type Bins = mutable.Map[Int, Bin]
  type Chips = ListBuffer[Int]

  val bots: Bots = mutable.Map[Int, ActorRef]()
  val bins: Bins = mutable.Map[Int, Bin]()

  private val actions = List.newBuilder[Action]
  def getActions: List[Action] = actions.result()

  val answers: mutable.Map[Int, Chips] = mutable.Map[Int, Chips]()

  object Instructions extends JavaTokenParsers {
    def parse(input: String): Boolean = {
      val parsed = parseAll(decode, input)
      parsed.successful
    }

    private def decode: Parser[Any] =
      rep(bot | give)

    private def bot: Parser[Any] =
      "value" ~ wholeNumber ~ "goes to bot" ~ wholeNumber ^^ {
        case _ ~ chip ~ _ ~ id =>
          actions += Give(chip.toInt, id.toInt)
      }

    private def give: Parser[Any] =
      "bot" ~ wholeNumber ~ "gives low to" ~ entity ~ "and high to" ~ entity ^^ {
        case _ ~ id ~ _ ~ lowEntity ~ _ ~ highEntity =>
          val botId = id.toInt
          val actor = system.actorOf(Props(new Bot(botId, lowEntity, highEntity)), name = s"bot$botId")
          bots += (botId -> actor)
      }

    private def entity: Parser[To] =
      "output" ~ wholeNumber ^^ { case _ ~ id => ToBin(id.toInt) } |
         "bot" ~ wholeNumber ^^ { case _ ~ id => ToBot(id.toInt) }
  }

  case class Chip(id: Int)

  class Bot(val id: Int, lowTo: To, highTo: To) extends Actor {
    val chips: Chips = ListBuffer[Int]()

    override def receive: Receive = {
      case Chip(nr) =>
        chips += nr
        if (chips.length == 2) {
          giveChip(chips.min, lowTo)
          giveChip(chips.max, highTo)
        }
        synchronized {
          answers += (id -> chips)
        }
        sender ! None // for 'completing' the ask pattern
    }

    def giveChip(chip: Int, to: To): Unit = {
      to match {
        case ToBot(botId) =>
          Await.result(bots(botId) ? Chip(chip), timeout.duration)
        case ToBin(binId) => synchronized {
          if (!bins.contains(binId))
            bins += (binId -> new Bin(binId))
          bins(binId).chips += chip
        }
      }
    }
  }

  class Bin(id: Int) {
    val chips: Chips = ListBuffer[Int]()
  }

  sealed trait Action
  case class Give(chip: Int, bot: Int) extends Action

  sealed trait To {
    val id: Int
  }
  case class ToBot(id: Int) extends To
  case class ToBin(id: Int) extends To

  case class Answer(botId: Int, chips: List[Int])

}