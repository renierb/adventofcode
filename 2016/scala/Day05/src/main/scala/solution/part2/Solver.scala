package solution.part2

import akka.actor._
import akka.routing.RoundRobinPool
import solution.{Calculate, PasswordItem, Result, Work}

import scala.annotation.tailrec
import scala.collection.mutable.ListBuffer
import scala.reflect.ClassTag

class Solver[T <: Actor :ClassTag](factory: () => T, nrOfWorkers: Int, nrOfIterations: Int, listener: ActorRef) extends Actor {

  private val router = context.actorOf(RoundRobinPool(nrOfWorkers).props(Props(factory())), "router")

  private var workSegments: Stream[Int] = (0 until Int.MaxValue by nrOfIterations).toStream
  private var answers = ListBuffer[PasswordItem]()

  private var activeWorkers = 0

  def receive: Receive = {
    case Calculate =>
      activeWorkers += 1

      val i = workSegments.head
      router ! Work(i, i + nrOfIterations)
      workSegments = workSegments.tail

    case Result(value) =>
      activeWorkers -= 1

      answers ++= value
      if (answers.length >= 8) {
        answers = getPassword(answers.sortBy(a => (a.codeIndex, a.index)))

        if (answers.length >= 8) {
          val result = answers
            .take(8)
            .map(_.code)
            .mkString
          listener ! result // Send the result to the listener
          context.stop(self) // Stops this actor and all its supervised children
        } else {
          self ! Calculate
        }
      } else {
        self ! Calculate
      }
  }

  @tailrec
  final def getPassword(sorted: Seq[PasswordItem], result: List[PasswordItem] = List()): ListBuffer[PasswordItem] = {
    if (sorted.isEmpty || result.length == 8) {
      ListBuffer(result.reverse:_*)
    } else {
      if (result.isEmpty || sorted.head.codeIndex > result.head.codeIndex)
        getPassword(sorted.tail, sorted.head :: result)
      else
        getPassword(sorted.tail, result)
    }
  }
}