import part1.Solver

object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines.mkString 

  val answer1 = new Solver(input, 40).solve
  println(s"Part 1: $answer1")

  val answer2 = new Solver(input, 400000).solve
  println(s"Part 2: $answer2")
}
