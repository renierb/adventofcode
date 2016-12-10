import org.scalatest._
import part1.Solver

class Part1UnitTests extends FunSuite {
  test("ADVENT") {
    assertResult(6)(new Solver("ADVENT").solve)
  }

  test("A(1x5)BC") {
    assertResult(7)(new Solver("A(1x5)BC").solve)
  }

  test("(3x3)XYZ") {
    assertResult(9)(new Solver("(3x3)XYZ").solve)
  }

  test("(6x1)(1x3)A") {
    assertResult(6)(new Solver("(6x1)(1x3)A").solve)
  }

  test("X(8x2)(3x3)ABCY") {
    assertResult(18)(new Solver("X(8x2)(3x3)ABCY").solve)
  }
}
