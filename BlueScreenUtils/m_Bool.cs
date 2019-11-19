// ReSharper disable InconsistentNaming
namespace Utils {
	public class m_Bool {
		private bool status { get; set; }
		private string message { get; set; }

		public m_Bool(bool status, string message) {
			this.status = status;
			this.message = message;
		}

		public m_Bool(bool Status) {
			status = Status;
			message = "";
		}

		public static implicit operator bool(m_Bool f) => f.status;

		public override string ToString() => message;

		public string Message() => ToString();
	}
}