
object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines.mkString

  val answer1 = new part1.Solver(input.split(',').map(_.trim)).solve
  println(s"Part1: $answer1 blocks away")

  val answer2 = new part2.Solver(input.split(',').map(_.trim)).solve
  println(s"Part2: $answer2 blocks away")
}