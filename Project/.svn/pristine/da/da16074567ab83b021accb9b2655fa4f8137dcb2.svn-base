using System;
using System.Collections;
using FastReport;

namespace LazyAtHome.Winform.FactoryPortal.UI.Test
{
	/// <summary>
	/// Summary description for FrxArrayList.
	/// </summary>
	public class FrxArrayList : ArrayList
	{
		private TfrxUserDataSetClass		m_ds;
		private int							m_CurrentItem;

		public FrxArrayList()
		{
			m_ds = new TfrxUserDataSetClass();

			m_ds.Name = "ArrayList";
			// Fields is CR/LF terminated tokens
			m_ds.Fields = "Field_1\r\nField_2\r\nField_3";

			m_ds.OnCheckEOF += new IfrxUserDataSetEventDispatcher_OnCheckEOFEventHandler(Event_OnCheckEOF);
			m_ds.OnFirst    += new IfrxUserDataSetEventDispatcher_OnFirstEventHandler(Event_OnFirst);
			m_ds.OnNext     += new IfrxUserDataSetEventDispatcher_OnNextEventHandler(Event_OnNext);
			m_ds.OnPrior    += new IfrxUserDataSetEventDispatcher_OnPriorEventHandler(Event_OnPrior);
			m_ds.OnGetValue += new IfrxUserDataSetEventDispatcher_OnGetValueEventHandler(Event_OnGetValue);
		}

		private void Event_OnCheckEOF(out bool IsEOF)
		{
			if ( m_CurrentItem >= 0 && m_CurrentItem < Count ) IsEOF = false; else IsEOF = true;
		}

		private void Event_OnFirst()
		{
			m_CurrentItem = 0;
		}

		private void Event_OnGetValue(object VarName, out object Value)
		{
			//
			// note 1: That FastReport does not support Decimal values
			// note 2: Var name is point to one of the Fields value
			//
			Value = this[m_CurrentItem];
		}

		private void Event_OnNext()
		{
			m_CurrentItem++;
		}

		private void Event_OnPrior()
		{
			m_CurrentItem--;
		}
	}
}
