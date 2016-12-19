package part1

trait DomainDef {

  type Floors = Vector[Set[Item]]
  type Moves = List[Move]

  sealed abstract class Move
  case object Up extends Move
  case object Down extends Move

  def isGoal(e: Elevator): Boolean = {
    e.floor == 3 && (0 to 2).forall(e.floors(_).isEmpty)
  }

  private val noMovement = List[Elevator]()

  case class Elevator(floor: Int, floors: Floors) {

    def up: List[Elevator] =
      if (floor < 3)
        moveItems(floor + 1, Up)
      else
        noMovement

    def down: List[Elevator] =
      if (floor > 0) {
        if (!(0 until floor).forall(floors(_).isEmpty))
          moveItems(floor - 1, Down)
        else
          noMovement
      } else {
        noMovement
      }

    private def moveItems(toFloor: Int, move: Move): List[Elevator] = {
      def moveCargo(cargo: Set[Item]) = {
        floors
          .updated(toFloor, floors(toFloor) ++ cargo)
          .updated(floor, floors(floor) -- cargo)
      }

      val items = floors(floor)
      (for {
        cargo1 <- items
        cargo2 <- items
        if move == Up || (move == Down && cargo1 == cargo2) // Optimization: Move only one item when going Down
      } yield Elevator(toFloor, moveCargo(Set(cargo1, cargo2)))).toList
    }

    private def adjacentFloors: List[Elevator] =
      if (isGoal(this))
        noMovement // Do not move if all items are on the top floor!
      else
        up ++ down

    def legalFloors: List[Elevator] =
      for (floor <- adjacentFloors if floor.isLegal) yield floor

    def isLegal: Boolean =
      floors(floor).nonEmpty &&
      floors.forall { floor =>
        floor.forall {
          case Microchip(isotope) =>
            floor.exists(hasGenerator(isotope)) || !floor.exists(hasGenerator)
          case _ =>
            true
        }
      }

    def hasGenerator(isotope: Char)(other: Item): Boolean = other match {
      case Generator(`isotope`) => true
      case _ => false
    }

    def hasGenerator(other: Item): Boolean = other match {
      case Generator(_) => true
      case _ => false
    }
  }
}
