package part1

import akka.actor.Actor

class Worker(val salt: String) extends Actor with DomainDef {

  def receive: Receive = {
    case FindTriplet(from, until) =>
      val answers = findTriplets(from, until)
      if (answers.nonEmpty)
        answers.reverse.foreach(sender ! _)
      else
        sender ! TripletAnswer(None, from, until)

    case FindFullHouse(value, from, until) =>
      val answer = findFullHouse(quintupletRegex(value), from, from + 1)
      if (answer.isDefined)
        sender ! answer.get
      else
        sender ! FullHouseAnswer(None, from, until)
  }
}