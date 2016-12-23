import part1.InputParser

object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines.drop(2).mkString

  val inputNodes = InputParser(input)

  val answer1 = new part1.Solver(inputNodes).solve
  println(s"Part1: $answer1")

  val answer2 = new part2.Solver(inputNodes).solve
  println(s"Part2: $answer2")
}
