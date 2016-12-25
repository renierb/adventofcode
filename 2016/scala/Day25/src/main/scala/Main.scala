
object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines.toStream

  val answer1 = new part1.Solver(input).solve
  println(s"Part1: $answer1")

  // No part2: The 50th star is given at the top of the antenna!
}
