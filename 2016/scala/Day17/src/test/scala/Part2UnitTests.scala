import org.scalatest._
import part1.{PasscodeTerrain, Solver}

class Part2UnitTests extends FunSuite {
  test("part 2: passcode ihgpwlah") {
    object Part1 extends Solver with PasscodeTerrain {
      override val passcode: String = "ihgpwlah"

      val startPos = Pos(0, 0)
      val goal = Pos(3, 3)
    }

    val steps = Part1.longestPath.length
    assertResult(370)(steps)
  }

  test("part 2: passcode kglvqrro") {
    object Part1 extends Solver with PasscodeTerrain {
      override val passcode: String = "kglvqrro"

      val startPos = Pos(0, 0)
      val goal = Pos(3, 3)
    }

    val steps = Part1.longestPath.length
    assertResult(492)(steps)
  }

  test("part 2: passcode ulqzkmiv") {
    object Part1 extends Solver with PasscodeTerrain {
      override val passcode: String = "ulqzkmiv"

      val startPos = Pos(0, 0)
      val goal = Pos(3, 3)
    }

    val steps = Part1.longestPath.length
    assertResult(830)(steps)
  }
}