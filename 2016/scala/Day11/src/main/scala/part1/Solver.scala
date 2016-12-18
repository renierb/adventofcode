package part1

trait Solver extends DomainDef {

  val startState: Elevator

  def floorsWithHistory(e: Elevator, history: List[Move]): Stream[(Elevator, List[Move])] = {
    for ((elevator, move) <- e.legalFloors.toStream) yield (elevator, move :: history)
  }

  def newFloorsOnly(adjacent: Stream[(Elevator, List[Move])],
                    explored: Set[Elevator]): Stream[(Elevator, List[Move])] = {
    for ((elevator, moveList) <- adjacent if !explored.contains(elevator)) yield (elevator, moveList)
  }

  private def from(initial: Stream[(Elevator, List[Move])],
                   explored: Set[Elevator]): Stream[(Elevator, List[Move])] = {
    if (initial.isEmpty)
      Stream.empty
    else {
      val moreExplored = explored ++ initial.map(_._1)

      val moreMovement = for {
        (elevator, moves) <- initial
        move <- newFloorsOnly(floorsWithHistory(elevator, moves), moreExplored)
      } yield move

      moreMovement #::: from(moreMovement, moreExplored)
    }
  }

  lazy val tripsFromStart: Stream[(Elevator, List[Move])] =
    from(Stream((startState, List[Move]())), Set())

  lazy val tripsToGoal: Stream[(Elevator, List[Move])] =
    tripsFromStart.filter {
      case (e, _) => isGoal(e)
    }

  lazy val solution: List[Move] = tripsToGoal match {
    case Stream.Empty => Nil
    case (_, moveList) #:: _ => moveList
  }
}