import org.scalatest._
import part1._

class Part1UnitTests extends FunSuite {
  test("part 1") {
    val instructions = Stream(
      "cpy 41 a",
      "inc a",
      "inc a",
      "dec a",
      "jnz a 2",
      "dec a"
    )

    val solver = new Solver(instructions)
    val actual = solver.solve
    assertResult(42)(actual)
  }
}