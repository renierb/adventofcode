import org.scalatest._
import part1._

class Part1UnitTests extends FunSuite {
  test("salt: abc, first triplet at index 18, but not a key") {
    val worker = new DomainDef {
      val salt: String = "abc"
    }

    val triplets = worker.findTriplets(0, 20)
    assert(triplets.length == 1)

    val tripletAnswer = triplets.head
    assertResult(18)(triplets.head.index)

    val answer = worker.findFullHouse(worker.quintupletRegex(tripletAnswer.value.get), 18, 19)
    assert(answer.isEmpty)
  }

  test("salt: abc, next triplet at index 39 and is a key") {
    val worker = new DomainDef {
      val salt: String = "abc"
    }

    val triplets = worker.findTriplets(19, 40)
    assert(triplets.length == 1)

    val tripletAnswer = triplets.head
    assertResult(39)(triplets.head.index)

    val answer = worker.findFullHouse(worker.quintupletRegex(tripletAnswer.value.get), 18, 19)
    assert(answer.nonEmpty)
    assert(answer.get.index == 816)
  }
}
