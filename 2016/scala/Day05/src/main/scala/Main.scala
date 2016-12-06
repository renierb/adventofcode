import akka.actor.{ActorSystem, Props}
import solution._

object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines.mkString

  val workRange = 10000
  val system = ActorSystem("AdventOfCode")

  // create the result listener, which will print the result and shutdown the system
  val listener = system.actorOf(Props[Listener], name = "listener")

  //part 1
  //val master = system.actorOf(Props(new part1.Solver[solution.part1.Worker](() => new solution.part1.Worker(input), 4, workRange, listener)), name = "master")

  //part 2
  val part1 = system.actorOf(Props(new part2.Solver[solution.part2.Worker](() => new solution.part2.Worker(input), 4, workRange, listener)), name = "master")

  // start the calculation
  part1 ! Calculate
  part1 ! Calculate
  part1 ! Calculate
  part1 ! Calculate
}
