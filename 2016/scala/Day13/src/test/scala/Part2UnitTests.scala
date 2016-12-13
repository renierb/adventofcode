import org.scalatest._
import part2._

class Part2UnitTests extends FunSuite {
  test("part 2") {
    object InfiniteLevel extends Solver with InfiniteTerrain {
      val magicNumber = 10
      val maxSteps: Int = 10

      val startPos = Pos(1, 1)
      val goal = Pos(7, 4)
    }

    val total = InfiniteLevel.solution
    assertResult(18)(total)
  }
}
