import org.scalatest._
import part2._

class Part2UnitTests extends FunSuite {
  test("salt: abc, first triplet at index 5, but not a key") {
    val worker = new DomainDef {
      val salt: String = "abc"
    }

    val triplets = worker.findTriplets(0, 9)
    assert(triplets.length == 1)

    val tripletAnswer = triplets.head
    assertResult(5)(triplets.head.index)

    val answer = worker.findFullHouse(worker.quintupletRegex(tripletAnswer.value.get), 18, 19)
    assert(answer.isEmpty)
  }

  test("salt: abc, next triplet at index 10 and is a key") {
    val worker = new DomainDef {
      val salt: String = "abc"
    }

    val triplets = worker.findTriplets(6, 20)
    assert(triplets.length == 1)

    val tripletAnswer = triplets.head
    assertResult(10)(triplets.head.index)

    val answer = worker.findFullHouse(worker.quintupletRegex(tripletAnswer.value.get), 18, 19)
    assert(answer.nonEmpty)
    assert(answer.get.index == 89)
  }
}
