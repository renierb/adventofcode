package part1

trait DomainDef {

  type Floors = List[Set[Item]]

  sealed abstract class Move
  case object Up extends Move
  case object Down extends Move

  def isGoal(e: Elevator): Boolean = {
    e.floor == 3 && (0 to 2).forall(e.items(_).isEmpty)
  }

  case class Elevator(floor: Int, items: Floors) {

    def up: List[(Elevator, Move)] =
      if (floor < 3)
        moveItems(floor + 1, Up)
      else
        List()

    def down: List[(Elevator, Move)] =
      if (floor > 0) {
        if (!(0 until floor).forall(items(_).isEmpty))
          moveItems(floor - 1, Down)
        else
          List()
      }
      else
        List()

    private def moveItems(toFloor: Int, move: Move): List[(Elevator, Move)] = {
      (for {
        i <- 2 to 1 by -1
        xs <- items(floor).subsets(i)
        newItems = items
          .updated(toFloor, items(toFloor) ++ xs)
          .updated(floor, items(floor).diff(xs))
      } yield (Elevator(toFloor, newItems), move)).toList
    }

    private def adjacentFloors: List[(Elevator, Move)] =
      if (isGoal(this)) List() else up ::: down

    def legalFloors: List[(Elevator, Move)] =
      for (floor <- adjacentFloors if floor._1.isLegal) yield floor

    def isLegal: Boolean = items.forall { floor =>
      floor.forall { item =>
        if (item.isMicrochip)
          floor.exists(hasGenerator(item)) || !floor.exists(_.isGenerator)
        else
          floor.groupBy(_.Id(0)).forall {
            case (_, xs: Set[Item]) =>
              if (xs.size == 1)
                xs.head.isGenerator
              else
                true // Microchip is paired with its Generator
          }
      }
    }

    def hasGenerator(item: Item)(other: Item): Boolean =
      other.isGenerator && other.Id(0) == item.Id(0)
  }
}
