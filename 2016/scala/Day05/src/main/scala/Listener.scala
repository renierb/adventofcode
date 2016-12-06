import akka.actor.Actor

class Listener extends Actor {
  def receive = {
    case password: String =>
      println("\n\tPassword: \t\t%s\n\tCalculation time: \t%s"
        .format(password, 0))
      context.system.terminate()
  }
}