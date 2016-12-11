
object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines.mkString

  val answer1 = new part1.Solver(input, Seq(61, 17)).solve
  println(s"Part 1: $answer1")

  val answer2 = new part2.Solver(input, Seq(61, 17)).solve
  println(s"Part 2: $answer2")
}
