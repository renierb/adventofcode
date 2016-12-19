package part1

trait Solver extends DomainDef {

  val startState: Elevator

  def legalFloors(e: Elevator, moves: Int = 0): Stream[(Elevator, Int)] = {
    (for (elevator <- e.legalFloors if moves < elevator.score / 1.5) yield (elevator, moves + 1)).toStream
  }

  def newFloorsOnly(adjacent: Stream[(Elevator, Int)],
                    explored: Set[Elevator]): Stream[(Elevator, Int)] = {
    for (elevator <- adjacent if !explored.contains(elevator._1)) yield elevator
  }

  private def from(initial: Stream[(Elevator, Int)],
                   explored: Set[Elevator]): Stream[(Elevator, Int)] = {
    if (initial.isEmpty)
      Stream.empty
    else {
      val moreExplored = explored ++ initial.map(_._1)

      val moreMovement = for {
        (elevator, moves) <- initial
        move <- newFloorsOnly(legalFloors(elevator, moves), moreExplored)
      } yield move

      moreMovement #::: from(moreMovement, moreExplored)
    }
  }

  lazy val tripsFromStart: Stream[(Elevator, Int)] =
    from((startState, 0) #:: Stream[(Elevator, Int)](), Set())

  lazy val tripsToGoal: Option[(Elevator, Int)] =
    tripsFromStart.collectFirst({
      case (e: Elevator, moves: Int) if isGoal(e) => (e, moves)
    })

  lazy val solution: Int = tripsToGoal match {
    case None =>
      0
    case Some((_, moves)) =>
      moves
  }
}