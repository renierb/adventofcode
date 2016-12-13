import org.scalatest._
import part1.{InfiniteTerrain, Solver}

class Part1UnitTests extends FunSuite {
  test("part 1") {
    object InfiniteLevel extends Solver with InfiniteTerrain {
      val magicNumber = 10

      val startPos = Pos(1, 1)
      val goal = Pos(7, 4)
    }

    val paths = InfiniteLevel.solution
    assertResult(11)(paths.length)
  }
}
