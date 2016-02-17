﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ejemplo14_01
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="FUTBOL2006")]
	public partial class FutbolDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertClub(Club instance);
    partial void UpdateClub(Club instance);
    partial void DeleteClub(Club instance);
    partial void InsertPais(Pais instance);
    partial void UpdatePais(Pais instance);
    partial void DeletePais(Pais instance);
    partial void InsertFutbolista(Futbolista instance);
    partial void UpdateFutbolista(Futbolista instance);
    partial void DeleteFutbolista(Futbolista instance);
    #endregion
		
		public FutbolDataContext() : 
				base(global::Ejemplo14_01.Properties.Settings.Default.FUTBOL2006ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public FutbolDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FutbolDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FutbolDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FutbolDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Club> Club
		{
			get
			{
				return this.GetTable<Club>();
			}
		}
		
		public System.Data.Linq.Table<Pais> Pais
		{
			get
			{
				return this.GetTable<Pais>();
			}
		}
		
		public System.Data.Linq.Table<Futbolista> Futbolista
		{
			get
			{
				return this.GetTable<Futbolista>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.InsertarFutbolista")]
		public int InsertarFutbolista([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Nombre", DbType="VarChar(75)")] string nombre, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="FechaNacimiento", DbType="DateTime")] System.Nullable<System.DateTime> fechaNacimiento, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="CodigoPaisNacimiento", DbType="Char(2)")] string codigoPaisNacimiento, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="CodigoClub", DbType="Char(3)")] string codigoClub, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Posicion", DbType="Char(1)")] System.Nullable<char> posicion)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), nombre, fechaNacimiento, codigoPaisNacimiento, codigoClub, posicion);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.FutbolistasDeUnPais", IsComposable=true)]
		public System.Nullable<int> FutbolistasDeUnPais([global::System.Data.Linq.Mapping.ParameterAttribute(Name="CodigoPais", DbType="Char(2)")] string codigoPais)
		{
			return ((System.Nullable<int>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), codigoPais).ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Club")]
	public partial class Club : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Codigo;
		
		private string _Nombre;
		
		private string _Ciudad;
		
		private EntitySet<Futbolista> _Futbolista;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCodigoChanging(string value);
    partial void OnCodigoChanged();
    partial void OnNombreChanging(string value);
    partial void OnNombreChanged();
    partial void OnCiudadChanging(string value);
    partial void OnCiudadChanged();
    #endregion
		
		public Club()
		{
			this._Futbolista = new EntitySet<Futbolista>(new Action<Futbolista>(this.attach_Futbolista), new Action<Futbolista>(this.detach_Futbolista));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Codigo", DbType="Char(3) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string Codigo
		{
			get
			{
				return this._Codigo;
			}
			set
			{
				if ((this._Codigo != value))
				{
					this.OnCodigoChanging(value);
					this.SendPropertyChanging();
					this._Codigo = value;
					this.SendPropertyChanged("Codigo");
					this.OnCodigoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nombre", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Nombre
		{
			get
			{
				return this._Nombre;
			}
			set
			{
				if ((this._Nombre != value))
				{
					this.OnNombreChanging(value);
					this.SendPropertyChanging();
					this._Nombre = value;
					this.SendPropertyChanged("Nombre");
					this.OnNombreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Ciudad", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Ciudad
		{
			get
			{
				return this._Ciudad;
			}
			set
			{
				if ((this._Ciudad != value))
				{
					this.OnCiudadChanging(value);
					this.SendPropertyChanging();
					this._Ciudad = value;
					this.SendPropertyChanged("Ciudad");
					this.OnCiudadChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Club_Futbolista", Storage="_Futbolista", ThisKey="Codigo", OtherKey="CodigoClub")]
		public EntitySet<Futbolista> Futbolista
		{
			get
			{
				return this._Futbolista;
			}
			set
			{
				this._Futbolista.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Futbolista(Futbolista entity)
		{
			this.SendPropertyChanging();
			entity.Club = this;
		}
		
		private void detach_Futbolista(Futbolista entity)
		{
			this.SendPropertyChanging();
			entity.Club = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Pais")]
	public partial class Pais : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Codigo;
		
		private string _Nombre;
		
		private EntitySet<Futbolista> _Futbolista;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCodigoChanging(string value);
    partial void OnCodigoChanged();
    partial void OnNombreChanging(string value);
    partial void OnNombreChanged();
    #endregion
		
		public Pais()
		{
			this._Futbolista = new EntitySet<Futbolista>(new Action<Futbolista>(this.attach_Futbolista), new Action<Futbolista>(this.detach_Futbolista));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Codigo", DbType="Char(2) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string Codigo
		{
			get
			{
				return this._Codigo;
			}
			set
			{
				if ((this._Codigo != value))
				{
					this.OnCodigoChanging(value);
					this.SendPropertyChanging();
					this._Codigo = value;
					this.SendPropertyChanged("Codigo");
					this.OnCodigoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nombre", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Nombre
		{
			get
			{
				return this._Nombre;
			}
			set
			{
				if ((this._Nombre != value))
				{
					this.OnNombreChanging(value);
					this.SendPropertyChanging();
					this._Nombre = value;
					this.SendPropertyChanged("Nombre");
					this.OnNombreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Pais_Futbolista", Storage="_Futbolista", ThisKey="Codigo", OtherKey="CodigoPaisNacimiento")]
		public EntitySet<Futbolista> Futbolista
		{
			get
			{
				return this._Futbolista;
			}
			set
			{
				this._Futbolista.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Futbolista(Futbolista entity)
		{
			this.SendPropertyChanging();
			entity.Pais = this;
		}
		
		private void detach_Futbolista(Futbolista entity)
		{
			this.SendPropertyChanging();
			entity.Pais = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Futbolista")]
	public partial class Futbolista : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Nombre;
		
		private char _Sexo;
		
		private System.Nullable<System.DateTime> _FechaNacimiento;
		
		private string _CodigoPaisNacimiento;
		
		private string _CodigoClub;
		
		private char _Posicion;
		
		private EntityRef<Club> _Club;
		
		private EntityRef<Pais> _Pais;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNombreChanging(string value);
    partial void OnNombreChanged();
    partial void OnSexoChanging(char value);
    partial void OnSexoChanged();
    partial void OnFechaNacimientoChanging(System.Nullable<System.DateTime> value);
    partial void OnFechaNacimientoChanged();
    partial void OnCodigoPaisNacimientoChanging(string value);
    partial void OnCodigoPaisNacimientoChanged();
    partial void OnCodigoClubChanging(string value);
    partial void OnCodigoClubChanged();
    partial void OnPosicionChanging(char value);
    partial void OnPosicionChanged();
    #endregion
		
		public Futbolista()
		{
			this._Club = default(EntityRef<Club>);
			this._Pais = default(EntityRef<Pais>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nombre", DbType="VarChar(75) NOT NULL", CanBeNull=false)]
		public string Nombre
		{
			get
			{
				return this._Nombre;
			}
			set
			{
				if ((this._Nombre != value))
				{
					this.OnNombreChanging(value);
					this.SendPropertyChanging();
					this._Nombre = value;
					this.SendPropertyChanged("Nombre");
					this.OnNombreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Sexo", DbType="Char(1) NOT NULL")]
		public char Sexo
		{
			get
			{
				return this._Sexo;
			}
			set
			{
				if ((this._Sexo != value))
				{
					this.OnSexoChanging(value);
					this.SendPropertyChanging();
					this._Sexo = value;
					this.SendPropertyChanged("Sexo");
					this.OnSexoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FechaNacimiento", DbType="DateTime")]
		public System.Nullable<System.DateTime> FechaNacimiento
		{
			get
			{
				return this._FechaNacimiento;
			}
			set
			{
				if ((this._FechaNacimiento != value))
				{
					this.OnFechaNacimientoChanging(value);
					this.SendPropertyChanging();
					this._FechaNacimiento = value;
					this.SendPropertyChanged("FechaNacimiento");
					this.OnFechaNacimientoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CodigoPaisNacimiento", DbType="Char(2) NOT NULL", CanBeNull=false)]
		public string CodigoPaisNacimiento
		{
			get
			{
				return this._CodigoPaisNacimiento;
			}
			set
			{
				if ((this._CodigoPaisNacimiento != value))
				{
					if (this._Pais.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCodigoPaisNacimientoChanging(value);
					this.SendPropertyChanging();
					this._CodigoPaisNacimiento = value;
					this.SendPropertyChanged("CodigoPaisNacimiento");
					this.OnCodigoPaisNacimientoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CodigoClub", DbType="Char(3) NOT NULL", CanBeNull=false)]
		public string CodigoClub
		{
			get
			{
				return this._CodigoClub;
			}
			set
			{
				if ((this._CodigoClub != value))
				{
					if (this._Club.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCodigoClubChanging(value);
					this.SendPropertyChanging();
					this._CodigoClub = value;
					this.SendPropertyChanged("CodigoClub");
					this.OnCodigoClubChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Posicion", DbType="Char(1) NOT NULL")]
		public char Posicion
		{
			get
			{
				return this._Posicion;
			}
			set
			{
				if ((this._Posicion != value))
				{
					this.OnPosicionChanging(value);
					this.SendPropertyChanging();
					this._Posicion = value;
					this.SendPropertyChanged("Posicion");
					this.OnPosicionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Club_Futbolista", Storage="_Club", ThisKey="CodigoClub", OtherKey="Codigo", IsForeignKey=true)]
		public Club Club
		{
			get
			{
				return this._Club.Entity;
			}
			set
			{
				Club previousValue = this._Club.Entity;
				if (((previousValue != value) 
							|| (this._Club.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Club.Entity = null;
						previousValue.Futbolista.Remove(this);
					}
					this._Club.Entity = value;
					if ((value != null))
					{
						value.Futbolista.Add(this);
						this._CodigoClub = value.Codigo;
					}
					else
					{
						this._CodigoClub = default(string);
					}
					this.SendPropertyChanged("Club");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Pais_Futbolista", Storage="_Pais", ThisKey="CodigoPaisNacimiento", OtherKey="Codigo", IsForeignKey=true)]
		public Pais Pais
		{
			get
			{
				return this._Pais.Entity;
			}
			set
			{
				Pais previousValue = this._Pais.Entity;
				if (((previousValue != value) 
							|| (this._Pais.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Pais.Entity = null;
						previousValue.Futbolista.Remove(this);
					}
					this._Pais.Entity = value;
					if ((value != null))
					{
						value.Futbolista.Add(this);
						this._CodigoPaisNacimiento = value.Codigo;
					}
					else
					{
						this._CodigoPaisNacimiento = default(string);
					}
					this.SendPropertyChanged("Pais");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591