
object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines.mkString

  object Part1 extends part1.Solver with part1.PasscodeTerrain {
    val passcode: String = input

    val startPos = Pos(0, 0)
    val goal = Pos(3, 3)
  }

  val answer1 = Part1.shortestPath
  println(s"Part1: ${answer1.reverse.mkString}")

  val answer2 = Part1.longestPath
  println(s"Part1: ${answer2.length}")
}
