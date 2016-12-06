import solution._

object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines

  //val answer1 = new part1.Solver(input.toSeq).solve
  //println(s"Part1: $answer1")

  val answer2 = new part2.Solver(input.toSeq).solve
  println(s"Part2: $answer2")
}
