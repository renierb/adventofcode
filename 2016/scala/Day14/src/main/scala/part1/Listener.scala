package part1

import akka.actor.Actor

class Listener extends Actor {
  def receive = {
    case Answer(index, duration, stop) =>
      println("\n\tIndex: \t\t\t%s\n\tCalculation time: \t%s".format(index, duration))

      if (stop) {
        context.system.terminate()
      }
  }
}