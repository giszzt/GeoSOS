/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2016/5/17
 * Time: 9:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using ICSharpCode.Core;

namespace FrameWork.Ribbon
{
	/// <summary>
	/// Description of RibbonDoozer.
	/// </summary>
	public class RibbonDoozer:IDoozer
	{
		public bool HandleConditions {
			get {
				return true;
			}
		}
		
		public object BuildItem(BuildItemArgs args)
		{
			return new RibbonItemDescriptor(args.Caller, args.Codon, args.BuildSubItems<object>(), args.Conditions);
		}
		
	}
	
	/// <summary>
	/// Represents a menu item. These objects are created by the RibbonDoozer and
	/// then converted into GUI-toolkit-specific objects by the RibbonService.
	/// </summary>
	public sealed class RibbonItemDescriptor
	{
		public readonly object Caller;
		public readonly Codon Codon;
		public readonly IList SubItems;
		public readonly IEnumerable<ICondition> Conditions;
		
		public RibbonItemDescriptor(object caller, Codon codon, IList subItems, IEnumerable<ICondition> conditions)
		{
			if (codon == null)
				throw new ArgumentNullException("codon");
			this.Caller = caller;
			this.Codon = codon;
			this.SubItems = subItems;
			this.Conditions = conditions;
		}
	}
}
