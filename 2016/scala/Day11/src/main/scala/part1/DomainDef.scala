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

  private val noMovement = List[(Elevator, Move)]()

  case class Elevator(floor: Int, floors: Floors) {

    def up: List[(Elevator, Move)] =
      if (floor < 3)
        moveItems(floor + 1, Up)
      else
        noMovement

    def down: List[(Elevator, Move)] =
      if (floor > 0) {
        if (!(0 until floor).forall(floors(_).isEmpty))
          moveItems(floor - 1, Down)
        else
          noMovement
      }
      else
        noMovement

    private def moveItems(toFloor: Int, move: Move): List[(Elevator, Move)] = {
      val maxItems = if (move == Up) 2 else 1
      (for {
        i <- maxItems to 1 by -1
        xs <- floors(floor).subsets(i)
        if move == Up || (move == Down && xs.forall(_.isMicrochip))
        newItems = floors
          .updated(toFloor, floors(toFloor) ++ xs)
          .updated(floor, floors(floor).diff(xs))
      } yield (Elevator(toFloor, newItems), move)).toList
    }

    private def adjacentFloors: List[(Elevator, Move)] =
      if (isGoal(this))
        noMovement // do not move if all items are on the top floor!
      else
        up ++ down

    def legalFloors: List[(Elevator, Move)] =
      for (floor <- adjacentFloors if floor._1.isLegal) yield floor

    def isLegal: Boolean =
      floors(floor).nonEmpty &&
      floors.forall { floor =>
        floor.forall { item =>
          if (item.isMicrochip)
            floor.exists(hasGenerator(item)) || !floor.exists(_.isGenerator)
          else
            floor.groupBy(_.name).forall {
              case (_, xs: Set[Item]) =>
                if (xs.size == 1)
                  xs.head.isGenerator
                else
                  true // Microchip is paired with its Generator
            }
        }
      }

    def hasGenerator(item: Item)(other: Item): Boolean =
      other.isGenerator && other.name == item.name
  }
}
