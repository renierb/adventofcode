import akka.actor.Actor
import solution.Answer

class Listener extends Actor {
  def receive = {
    case Answer(password, duration, stop) =>
      println("\n\tPassword: \t\t%s\n\tCalculation time: \t%s".format(password, duration))

      if (stop) {
        context.system.terminate()
      }
  }
}