import part1.Solver

object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines.mkString 

  val answer1 = new Solver(input).solve
  println(s"Part1: $answer1")
}
