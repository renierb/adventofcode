package part1

import com.twmacinta.util.MD5

import scala.annotation.tailrec
import scala.util.matching.Regex

trait DomainDef {

  val salt: String

  protected val md5 = new MD5()

  private val tripletRegex = """(.)\1{2}""".r

  def quintupletRegex(value: String): Regex = {
    s"${value(0)}{5}".r
  }

  @tailrec
  final def findTriplets(from: Int, until: Int, answers: List[TripletAnswer] = List()): List[TripletAnswer] = {
    if (from >= until) {
      answers
    } else {
      val md5 = MD5(s"$salt$from")
      val result = tripletRegex.findFirstMatchIn(md5)
      if (result.isDefined) {
        findTriplets(from + 1, until, TripletAnswer(Option(result.get.matched), from, until) :: answers)
      } else {
        findTriplets(from + 1, until, answers)
      }
    }
  }

  @tailrec
  final def findFullHouse(regex: Regex, from: Int, index: Int): Option[FullHouseAnswer] = {
    if (index >= from + 1000) {
      None
    } else {
      val md5 = MD5(s"$salt$index")
      val result = regex.findFirstMatchIn(md5)
      if (result.isDefined) {
        Some(FullHouseAnswer(Option(result.get.matched), from, index))
      } else {
        findFullHouse(regex, from, index + 1)
      }
    }
  }

  protected def MD5(text: String, count: Int = 0): String = {
    md5.Init()
    md5.Update(text)
    md5.asHex()
  }
}
