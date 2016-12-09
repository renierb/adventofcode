import org.scalatest._
import part1.Solver

class Part1UnitTests extends FunSuite {
  test("part 1") {
    val solver = new Solver(Stream(
      Array("rect", "3x2"),
      Array("rotate", "column", "x=1", "by", "1"),
      Array("rotate", "row", "y=0", "by", "4"),
      Array("rotate", "column", "x=1", "by", "1")
    ), 7, 3)

    val screen = solver.solve
    assertResult(6)(solver.pixels(screen))
  }
}
