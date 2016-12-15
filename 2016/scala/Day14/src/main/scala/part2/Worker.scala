package part2

import akka.actor.Actor
import part1.{FindFullHouse, FindTriplet, FullHouseAnswer, TripletAnswer}

class Worker(val salt: String) extends Actor with DomainDef {

  def receive: Receive = {
    case FindTriplet(from, until) =>
      val answers = findTriplets(from, until)
      if (answers.nonEmpty)
        answers.foreach(sender ! _)
      else
        sender ! TripletAnswer(None, from, until)

    case FindFullHouse(value, from, until) =>
      val answer = findFullHouse(s"${value(0)}{5}".r, from, from + 1)
      if (answer.isDefined)
        sender ! answer.get
      else
        sender ! FullHouseAnswer(None, from, until)
  }
}