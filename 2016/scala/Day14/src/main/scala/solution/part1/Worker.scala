package solution.part1

import java.security.MessageDigest

import akka.actor.Actor
import solution._

import scala.annotation.tailrec
import scala.util.matching.Regex

class Worker(salt: String) extends Actor {
  private val digest = MessageDigest.getInstance("MD5")

  def receive: Receive = {
    case FindTriplet(from, until) =>
      //println(s"Receive request: $from until $until")
      val answers = findTriplets("""(.)\1{2}""".r, from, until)
      if (answers.nonEmpty)
        answers.reverse.foreach(sender ! _)
      else
        sender ! TripletAnswer(None, from, until)

    case FindFullHouse(value, from, until) =>
      val answer = findFullHouse(s"${value(0)}{5}".r, from, from + 1)
      if (answer.isDefined)
        sender ! answer.get
      else
        sender ! FullHouseAnswer(None, from, until)

  }

  @tailrec
  final def findTriplets(regex: Regex, from: Int, until: Int, answers: List[TripletAnswer] = List()): List[TripletAnswer] = {
    if (from >= until) {
      answers
    } else {
      val md5 = MD5(digest, s"$salt$from")
      val result = regex.findFirstMatchIn(md5)
      if (result.isDefined) {
        findTriplets(regex, from + 1, until, TripletAnswer(Option(result.get.matched), from, until) :: answers)
      } else {
        findTriplets(regex, from + 1, until, answers)
      }
    }
  }

  @tailrec
  final def findFullHouse(regex: Regex, from: Int, index: Int): Option[FullHouseAnswer] = {
    if (index >= from + 1000) {
      None
    } else {
      val md5 = MD5(digest, s"$salt$index")
      val result = regex.findFirstMatchIn(md5)
      if (result.isDefined) {
        Some(FullHouseAnswer(Option(result.get.matched), from, index))
      } else {
        findFullHouse(regex, from, index + 1)
      }
    }
  }

  private def MD5(digest: MessageDigest, text: String): String = {
    digest.digest(text.getBytes).map("%02x".format(_)).mkString
  }
}