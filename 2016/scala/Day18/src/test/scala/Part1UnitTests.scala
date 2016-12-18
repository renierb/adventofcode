import org.scalatest._
import part1.Solver

class Part1UnitTests extends FunSuite {
  test("part 1: ..^^.") {
    val solver = new Solver("..^^.", 3)
    val actual = solver.solve
    assertResult(6)(actual)
  }

  test("part 1: .^^.^.^^^^") {
    val solver = new Solver(".^^.^.^^^^", 10)
    val actual = solver.solve
    assertResult(38)(actual)
  }
}
