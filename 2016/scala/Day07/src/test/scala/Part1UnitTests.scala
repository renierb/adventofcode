import org.scalatest._
import org.scalatest.prop.TableDrivenPropertyChecks
import solution.part1.Solver

class Part1UnitTests extends FunSuite with TableDrivenPropertyChecks {

  test("supports TLS (abba outside square brackets)") {
    val solver = new Solver(Stream("abba[mnop]qrst"))
    val actual = solver.solve
    assertResult(1)(actual)
  }

  test("does not support TLS (bddb is within bracket)") {
    val solver = new Solver(Stream("abcd[bddb]xyyx"))
    val actual = solver.solve
    assertResult(0)(actual)
  }

  test("does not support TLS (aaaa is invalid)") {
    val solver = new Solver(Stream("aaaa[qwer]tyui"))
    val actual = solver.solve
    assertResult(0)(actual)
  }

  test("does not support TLS (aabb is invalid)") {
    val solver = new Solver(Stream("tyui[qwer]aabb"))
    val actual = solver.solve
    assertResult(0)(actual)
  }

  test("supports TLS (oxxo is outside square brackets)") {
    val solver = new Solver(Stream("ioxxoj[asdfgh]zxcvbn"))
    val actual = solver.solve
    assertResult(1)(actual)
  }
}
