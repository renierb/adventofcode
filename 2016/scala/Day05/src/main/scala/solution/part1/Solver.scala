package solution.part1

import akka.actor._
import akka.routing.RoundRobinPool
import scala.concurrent.duration._
import solution._

import scala.reflect._

class Solver[T <: Actor :ClassTag](factory: () => T, nrOfWorkers: Int, nrOfIterations: Int, listener: ActorRef) extends Actor {

  private var router = context.actorOf(RoundRobinPool(nrOfWorkers).props(Props(factory())), "router")
  private val start: Long = System.currentTimeMillis

  private var nrOfAnswers: Int = _
  private var workSegments: Stream[Int] = (0 until Int.MaxValue by nrOfIterations).toStream

  private val answers = List.newBuilder[PasswordItem]

  def receive: Receive = {
    case Calculate => {
      val i = workSegments.head
      router ! Work(i, i + nrOfIterations)
      workSegments = workSegments.tail
    }

    case Result(value) =>
      nrOfAnswers += value.length
      answers ++= value
      if (nrOfAnswers >= 8) {
        val result = answers.result
          .sortBy(_.index)
          .take(8)
          .map(_.code)
          .mkString

        listener ! Answer(result, duration = (System.currentTimeMillis - start).millis)
        context.stop(self) // Stops this actor and all its supervised children
      } else {
        self ! Calculate
      }
  }
}