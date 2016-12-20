import scala.collection.immutable.SortedSet

object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines.toStream
  val inputMap = SortedSet(input.map(_.split('-').map(_.toLong)).map(p => (p(0), p(1))): _*)

  val answer1 = new part1.Solver(inputMap).solve
  println(s"Part1: $answer1")

  val answer2 = new part2.Solver(inputMap).solve
  println(s"Part2: $answer2")
}
