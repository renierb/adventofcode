package part1

import akka.actor._
import akka.routing.RoundRobinPool

import scala.concurrent.duration._

import scala.collection.mutable
import scala.reflect._

class Solver[T <: Actor :ClassTag](factory: () => T, nrOfWorkers: Int, nrOfIterations: Int, listener: ActorRef) extends Actor {

  private val router = context.actorOf(RoundRobinPool(nrOfWorkers).props(Props(factory())), "router")
  private val start: Long = System.currentTimeMillis

  private var nrOfAnswers: Int = _
  private var workSegments: Stream[Int] = (0 until Int.MaxValue by nrOfIterations).toStream

  private val answers = mutable.Map[Int, Option[PasswordItem]]()

  def receive: Receive = {
    case Calculate =>
      val from = workSegments.head
      val until = from + nrOfIterations
      router ! FindTriplet(from, until)
      workSegments = workSegments.tail

    case TripletAnswer(value, index, until) =>
      if (value.nonEmpty) {
        answers += (index -> None)
        router ! FindFullHouse(value.get, index, until)
      } else {
        self ! Calculate
      }

    case FullHouseAnswer(value, from, index) =>
      if (value.isEmpty) {
        answers -= from
        self ! Calculate
      } else {
        answers += (from -> Some(PasswordItem(index)))
        val sorted = answers.toList.sortBy(_._1).take(64)
        nrOfAnswers = sorted.count(_._2.isDefined)
        if (nrOfAnswers >= 64) {
          val answer = sorted.last._1
          listener ! Answer(answer, duration = (System.currentTimeMillis - start).millis)
          context.stop(self) // Stops this actor and all its supervised children
        } else {
          self ! Calculate
        }
      }
  }
}