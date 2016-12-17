import org.scalatest._
import part1.{PasscodeTerrain, Solver}

class Part1UnitTests extends FunSuite {
  test("part 1: passcode hijkl") {
    object Part1 extends Solver with PasscodeTerrain {
      override val passcode: String = "hijkl"

      val startPos = Pos(0, 0)
      val goal = Pos(3, 3)
    }

    val steps = Part1.shortestPath
    assertResult(List())(steps)
  }

  test("part 1: passcode ihgpwlah") {
    object Part1 extends Solver with PasscodeTerrain {
      override val passcode: String = "ihgpwlah"

      val startPos = Pos(0, 0)
      val goal = Pos(3, 3)
    }

    val steps = Part1.shortestPath
    assertResult("DDRRRD")(steps.reverse.mkString)
  }

  test("part 1: passcode kglvqrro") {
    object Part1 extends Solver with PasscodeTerrain {
      override val passcode: String = "kglvqrro"

      val startPos = Pos(0, 0)
      val goal = Pos(3, 3)
    }

    val steps = Part1.shortestPath
    assertResult("DDUDRLRRUDRD")(steps.reverse.mkString)
  }

  test("part 1: passcode ulqzkmiv") {
    object Part1 extends Solver with PasscodeTerrain {
      override val passcode: String = "ulqzkmiv"

      val startPos = Pos(0, 0)
      val goal = Pos(3, 3)
    }

    val steps = Part1.shortestPath
    assertResult("DRURDRUDDLLDLUURRDULRLDUUDDDRR")(steps.reverse.mkString)
  }
}
