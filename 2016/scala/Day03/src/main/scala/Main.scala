import solution._

object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines.toSeq

  val rowTriangles: Seq[Array[Int]] = input.map(_.trim.split(' ').filter(_.length > 0).map(_.toInt))
  val answer1 = new part1.Solver(rowTriangles).solve
  println(s"Part1: $answer1")

  val answer2 = new part2.Solver(rowTriangles).solve
  println(s"Part2: $answer2")
}
