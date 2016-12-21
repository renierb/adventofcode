import part1.InputParser.Disc
import part1.{InputParser, Solver}

object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines.mkString 

  val inputDiscs = InputParser(input)
  val answer1 = new Solver(inputDiscs).solve
  println(s"Part1: $answer1")

  val newDiscs = inputDiscs ++ List(Disc(inputDiscs.length + 1, 11, 0))
  val answer2 = new Solver(newDiscs).solve
  println(s"Part2: $answer2")
}
