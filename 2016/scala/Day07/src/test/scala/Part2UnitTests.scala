import org.scalatest._
import solution.part2.Solver

class Part2UnitTests extends FunSuite {
  test("supports SSL (aba outside square brackets with corresponding bab within square brackets)") {
    val solver = new Solver(Stream("aba[bab]xyz)"))
    val actual = solver.solve
    assertResult(1)(actual)
  }

  test("does not support SSL (xyx, but no corresponding yxy)") {
    val solver = new Solver(Stream("xyx[xyx]xyx"))
    val actual = solver.solve
    assertResult(0)(actual)
  }

  test("supports SSL (eke in supernet with corresponding kek in hypernet)") {
    val solver = new Solver(Stream("aaa[kek]eke"))
    val actual = solver.solve
    assertResult(1)(actual)
  }

  test("supports SSL (zaz has no corresponding aza, but zbz has a corresponding bzb)") {
    val solver = new Solver(Stream("zazbz[bzb]cdb"))
    val actual = solver.solve
    assertResult(1)(actual)
  }
}
