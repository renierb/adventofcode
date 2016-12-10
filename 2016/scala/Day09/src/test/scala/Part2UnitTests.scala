import org.scalatest._
import part2.Solver

class Part2UnitTests extends FunSuite {
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
    assertResult(3)(new Solver("(6x1)(1x3)A").solve)
  }

  test("(12x2)B(6x2)(1x3)A") {
    assertResult(14)(new Solver("(12x2)B(6x2)(1x3)A").solve)
  }

  test("X(8x2)(3x3)ABCY") {
    assertResult("XABCABCABCABCABCABCY".length)(new Solver("X(8x2)(3x3)ABCY").solve)
  }

  test("(27x12)(20x12)(13x14)(7x10)(1x12)A") {
    assertResult(241920)(new Solver("(27x12)(20x12)(13x14)(7x10)(1x12)A").solve)
  }

  test("(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN") {
    assertResult(445)(new Solver("(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN").solve)
  }
}
