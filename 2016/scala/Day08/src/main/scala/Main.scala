
object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines
  val instructions = input.map(_.split(' ')).toStream

  val answer1 = new part1.Solver(instructions).solve
  println(s"Part1: Pixels lit $answer1")

  //val answer2 = new part2.Solver(input).solve()
  //println(s"Part1: Code is ${answer2.mkString}")
}
