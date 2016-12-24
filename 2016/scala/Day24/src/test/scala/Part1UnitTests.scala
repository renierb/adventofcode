import org.scalatest._
import part1.Solver

class Part1UnitTests extends FunSuite {
  test("part 1") {
    val input = Vector(
      "###########",
      "#0.1.....2#",
      "#.#######.#",
      "#4.......3#",
      "###########"
    )

    val solver = new Solver(input, 4)
    assertResult((1, 1))(solver.startPos)

    val actual = solver.solve
    assertResult(14)(actual)
  }
}
