import akka.actor.{ActorSystem, Props}
import solution._

object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines.mkString

  val nrOfWorkers = 4
  val workRange = 10000
  val system = ActorSystem("AdventOfCode")

  // create the result listener, which will print the result and shutdown the system
  val listener = system.actorOf(Props[Listener], name = "listener")

  //part 1
  val part1Master = system.actorOf(Props(new part1.Solver[solution.part1.Worker](() => new solution.part1.Worker(input), nrOfWorkers, workRange, listener)), name = "master1")

  // start the calculation
  (0 until nrOfWorkers).foreach(_ => part1Master ! Calculate)

  //part 2
  val part2Master = system.actorOf(Props(new part2.Solver[solution.part2.Worker](() => new solution.part2.Worker(input), nrOfWorkers, workRange, listener)), name = "master2")

  // start the calculation
  (0 until nrOfWorkers).foreach(_ => part2Master ! Calculate)
}
