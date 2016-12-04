import org.scalatest._
import org.scalatest.prop.TableDrivenPropertyChecks
import part1.Solver

class Part1UnitTests extends FunSuite with TableDrivenPropertyChecks {
  val instructions =
    Table(
      ("instruction", "distance"),
      ("R2, L3", 5),
      ("R2, R2, R2", 2),
      ("R5, L5, R5, R3", 12)
    )

  test("distance to hq, i.e. from final position to Pos(0, 0)") {
    forAll (instructions) { (instructions, expected) =>
      val solver = new Solver(instructions.split(',').map(_.trim))
      val actual = solver.solve
      assertResult(expected)(actual)
    }
  }
}
