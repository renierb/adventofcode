import solution._

object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines.toStream

  val answer1 = new part1.Solver(input).solve
  println(s"Part1: $answer1")

  val answer2 = new part2.Solver(input).solve
  println(s"Part1: $answer2")
}
