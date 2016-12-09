package part1

class Solver(input: Stream[Array[String]], override val width: Int = 50, override val height: Int = 6) extends DomainDef {
  import Solver._

  private val screen = Array.fill[Pixel](height * width)(Pixel(-1, -1))

  private val indexes =
    for {
      row <- 0 until height
      col <- 0 until width
    } yield (row, col)

  indexes.foreach { case (r, c) =>
    screen(r * width + c) = Pixel(r, c)
  }

  def solve: Screen = {
    input.foldLeft(screen) { case (s, op) =>
      getOperation(op) match {
        case FILL_RECT =>
          val dims = getRectDimensions(op)
          Rect(s)(dims(0), dims(1))
        case ROTATE =>
          getRotateAction(op) match {
            case ROTATE_ROW =>
              RowAction(s).right(getPosition(op), getShiftBy(op))
            case ROTATE_COL =>
              ColAction(s).down(getPosition(op), getShiftBy(op))
          }
      }
    }
  }

  def pixels(screen: Screen): Int = {
    screen.count(_.on)
  }
}

object Solver {
  private val FILL_RECT = "rect"
  private val ROTATE = "rotate"
  private val ROTATE_ROW = "row"
  private val ROTATE_COL = "column"

  private def getOperation(i: Array[String]) = i(0)
  private def getRotateAction(i: Array[String]) = i(1)
  private def getRectDimensions(i: Array[String]) = i(1).split('x').map(_.toInt)
  private def getPosition(i: Array[String]) = i(2).split('=')(1).toInt
  private def getShiftBy(i: Array[String]) = i(4).toInt
}