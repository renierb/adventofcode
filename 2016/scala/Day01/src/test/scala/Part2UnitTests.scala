import org.scalatest._
import org.scalatest.prop.TableDrivenPropertyChecks
import part2.Solver

class Part2UnitTests extends FunSuite with TableDrivenPropertyChecks {
  val instructions =
    Table(
      ("instruction", "distance"),
      ("R2, L3", 0),
      ("R2, R2, R2", 0),
      ("R5, L5, R5, R3", 0),
      ("R8, R4, R4, R8", 4),
      ("R2, L2, R2, R3, R1, R2, R2", 5),
      ("R2, L2, R2, R3, R1, R2, L2, L2, R2", 3)
    )

  test("distance to hq, i.e. from Pos(0, 0) to first place visited twice") {
    forAll (instructions) { (instructions, expected) =>
      val solver = new Solver(instructions.split(',').map(_.trim))
      val actual = solver.solve
      assertResult(expected)(actual)
    }
  }
}
