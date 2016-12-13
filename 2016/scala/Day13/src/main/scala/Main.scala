
object Main extends App {
  //val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines

  object Part1 extends part1.Solver with part1.InfiniteTerrain {
    val magicNumber = 1364

    val startPos = Pos(1, 1)
    val goal = Pos(31, 39)
  }

  val path = Part1.solution
  println(s"Part1: ${path.length}")

  object Part2 extends part2.Solver with part2.InfiniteTerrain {
    val magicNumber = 1364
    val maxSteps: Int = 50

    val startPos = Pos(1, 1)
    val goal = Pos(31, 39)
  }

  val total = Part2.solution
  println(s"Part2: $total")
}
