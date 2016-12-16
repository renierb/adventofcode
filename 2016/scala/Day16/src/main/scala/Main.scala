
object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines.mkString 

  val answer1 = new part1.Solver(input, 272).solve
  println(s"Part1: $answer1")

  val answer2 = new part2.Solver(input, 35651584).solve
  println(s"Part2: $answer2")
}
