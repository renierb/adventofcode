
object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines.mkString

  val answer1 = new part1.Solver(input).solve
  println(s"Part1: Code is $answer1")
}
