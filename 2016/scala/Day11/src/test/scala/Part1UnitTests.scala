import org.scalatest._
import part1._

class Part1UnitTests extends FunSuite {
  test("part 1: example") {
    val input =
      """
        |The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip.
        |The second floor contains a hydrogen generator.
        |The third floor contains a lithium generator.
        |The fourth floor contains nothing relevant.
      """.stripMargin

    object Part1 extends part1.Solver {
      val floors = InputParser(input)
      override val startState = Elevator(0, floors.sortBy(_.nr).map(f => f.items.toSet).toVector)
    }

    val actual = Part1.solution
    assertResult(11)(actual)
  }

  test("part 1: start state is legal") {
    val impl = new DomainDef {
      private val floors = List[Set[Item]](
        Set(Microchip('H'), Microchip('L')),
        Set(Generator('H')),
        Set(Generator('L')),
        Set()
      )
      val elevator = Elevator(0, floors.toVector)
    }

    assert(impl.elevator.isLegal)
  }

  test("part 1: illegal state when pairing HG with LM") {
    val impl = new DomainDef {
      private val floors = List[Set[Item]](
        Set(Microchip('H')),
        Set(Generator('H'), Microchip('L')),
        Set(Generator('L')),
        Set()
      )
      val elevator = Elevator(0, floors.toVector)
    }

    assert(!impl.elevator.isLegal)
  }

  test("part 1: legal state when pairing HG+HM with LG") {
    val impl = new DomainDef {
      private val floors = List[Set[Item]](
        Set(Microchip('L')),
        Set(),
        Set(Generator('L'), Generator('H'), Microchip('H')),
        Set()
      )
      val elevator = Elevator(0, floors.toVector)
    }

    assert(impl.elevator.isLegal)
  }

  test("part 1: legal state when pairing HG+LG and HM+LM") {
    val impl = new DomainDef {
      private val floors = List[Set[Item]](
        Set(),
        Set(Microchip('L'), Microchip('H')),
        Set(Generator('L'), Generator('H')),
        Set()
      )
      val elevator = Elevator(1, floors.toVector)
    }

    assert(impl.elevator.isLegal)
  }

  test("part 1: illegal state when pairing HG+HM with LM") {
    val impl = new DomainDef {
      private val floors = List[Set[Item]](
        Set(),
        Set(Generator('H'), Microchip('H'), Microchip('L')),
        Set(Generator('L')),
        Set()
      )
      val elevator = Elevator(0, floors.toVector)
    }

    assert(!impl.elevator.isLegal)
    assert(impl.elevator.score == 9)
  }

  test("part 1: legal state when all items on top floor") {
    val impl = new DomainDef {
      private val floors = List[Set[Item]](
        Set(),
        Set(),
        Set(),
        Set(Generator('L'), Generator('H'), Microchip('L'), Microchip('H'))
      )
      val elevator = Elevator(3, floors.toVector)
    }

    assert(impl.elevator.isLegal)
    assert(impl.elevator.score == 16)
  }

  test("part 1: elevators are the same") {
    val impl = new DomainDef {
      val floors1: Floors = Vector[Set[Item]](
        Set(Microchip('H'), Microchip('L')),
        Set(Generator('H')),
        Set(Generator('L')),
        Set()
      )
      val floors2: Floors = Vector[Set[Item]](
        Set(Microchip('H'), Microchip('L')),
        Set(Generator('H')),
        Set(Generator('L')),
        Set()
      )
      val floors3: Floors = Vector[Set[Item]](
        Set(Generator('H')),
        Set(Microchip('H'), Microchip('L')),
        Set(Generator('L')),
        Set()
      )
      val elevator1 = Elevator(0, floors1)
      val elevator2 = Elevator(0, floors2)
    }

    assert(impl.floors1 == impl.floors2)
    assert(impl.floors1 != impl.floors3)
    assert(impl.elevator1 == impl.elevator2)
  }
}
