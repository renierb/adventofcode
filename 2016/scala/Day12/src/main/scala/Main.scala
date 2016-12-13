import part1.{InfiniteTerrain, Solver}

object Main extends App {
  //val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines

  object InfiniteLevel extends Solver with InfiniteTerrain {
    val magicNumber = 1364

    val startPos = Pos(1, 1)
    val goal = Pos(31, 39)
  }

  val path = InfiniteLevel.solution
  println(s"Part1: ${path.length}")
}
