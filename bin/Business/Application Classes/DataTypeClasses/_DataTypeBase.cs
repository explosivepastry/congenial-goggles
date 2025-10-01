// Decompiled with JetBrains decompiler
// Type: Monnit.DataTypeBase
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using Monnit.Application_Classes.DataTypeClasses;
using RedefineImpossible;
using System;
using System.Reflection;

#nullable disable
namespace Monnit;

public abstract class DataTypeBase : IEquatable<DataTypeBase>
{
  public abstract eDatumType datatype { get; }

  public abstract object compvalue { get; }

  public static bool HasStaticMethod(eDatumType edt, string methodname)
  {
    return AppDatum.getType(edt).GetMethod(methodname, BindingFlags.Static | BindingFlags.Public) != (MethodInfo) null;
  }

  public override bool Equals(object obj)
  {
    return obj.GetType() == typeof (DataTypeBase) ? (obj as DataTypeBase).compvalue == this.compvalue : base.Equals(obj);
  }

  public static bool operator >(DataTypeBase d1, DataTypeBase d2)
  {
    if (d1.datatype != d2.datatype)
      throw new InvalidCastException();
    switch (d1.datatype)
    {
      case eDatumType.BooleanData:
      case eDatumType.Data:
      case eDatumType.Status:
      case eDatumType.DoorData:
      case eDatumType.WaterDetect:
      case eDatumType.VoltageDetect:
      case eDatumType.ActivityDetect:
      case eDatumType.ControlDetect:
      case eDatumType.ButtonPressed:
      case eDatumType.DryContact:
      case eDatumType.LightDetect:
      case eDatumType.MagnetDetect:
      case eDatumType.MotionDetect:
      case eDatumType.SeatOccupied:
      case eDatumType.AccelerometerImpact:
      case eDatumType.AirflowDetect:
      case eDatumType.VehicleDetect:
      case eDatumType.CarDetected:
      case eDatumType.ProbeStatus:
      case eDatumType.DeviceID:
      case eDatumType.MilliVoltsPerVolts:
        throw new InvalidOperationException();
      case eDatumType.Percentage:
      case eDatumType.TemperatureData:
      case eDatumType.Time:
      case eDatumType.Voltage:
      case eDatumType.ResistanceData:
      case eDatumType.Pressure:
      case eDatumType.Angle:
      case eDatumType.MilliAmps:
      case eDatumType.Coordinate:
      case eDatumType.Distance:
      case eDatumType.Error:
      case eDatumType.Geforce:
      case eDatumType.LuxData:
      case eDatumType.Magnitude:
      case eDatumType.PPM:
      case eDatumType.Weight:
      case eDatumType.Speed:
      case eDatumType.WattHours:
      case eDatumType.Frequency:
      case eDatumType.Amps:
      case eDatumType.AmpHours:
      case eDatumType.UnscaledVoltage:
      case eDatumType.Voltage0To5:
      case eDatumType.Voltage0To10:
      case eDatumType.Voltage0To500:
      case eDatumType.Voltage0To1Point2:
      case eDatumType.Millimeter:
      case eDatumType.Micrograms:
      case eDatumType.Pascal:
      case eDatumType.CrestFactor:
      case eDatumType.Centimeter:
      case eDatumType.Voltage0To200:
      case eDatumType.Acceleration:
      case eDatumType.PPB:
      case eDatumType.MoistureWeight:
      case eDatumType.DifferentialPressureData:
      case eDatumType.MoistureTension:
      case eDatumType.AirSpeedData:
      case eDatumType.PPFDData:
      case eDatumType.PAR_DLI:
      case eDatumType.Decible:
      case eDatumType.Decibel:
        return d1.compvalue.ToDouble() > d2.compvalue.ToDouble();
      case eDatumType.Count:
      case eDatumType.State:
        return d1.compvalue.ToInt() > d2.compvalue.ToInt();
      default:
        throw new NotImplementedException();
    }
  }

  public static bool operator >=(DataTypeBase d1, DataTypeBase d2)
  {
    if (d1.datatype != d2.datatype)
      throw new InvalidCastException();
    switch (d1.datatype)
    {
      case eDatumType.BooleanData:
      case eDatumType.Data:
      case eDatumType.Status:
      case eDatumType.DoorData:
      case eDatumType.WaterDetect:
      case eDatumType.VoltageDetect:
      case eDatumType.ActivityDetect:
      case eDatumType.ControlDetect:
      case eDatumType.ButtonPressed:
      case eDatumType.DryContact:
      case eDatumType.LightDetect:
      case eDatumType.MagnetDetect:
      case eDatumType.MotionDetect:
      case eDatumType.SeatOccupied:
      case eDatumType.AccelerometerImpact:
      case eDatumType.AirflowDetect:
      case eDatumType.VehicleDetect:
      case eDatumType.CarDetected:
      case eDatumType.ProbeStatus:
      case eDatumType.DeviceID:
      case eDatumType.MilliVoltsPerVolts:
        throw new InvalidOperationException();
      case eDatumType.Percentage:
      case eDatumType.TemperatureData:
      case eDatumType.Time:
      case eDatumType.Voltage:
      case eDatumType.ResistanceData:
      case eDatumType.Pressure:
      case eDatumType.Angle:
      case eDatumType.MilliAmps:
      case eDatumType.Coordinate:
      case eDatumType.Distance:
      case eDatumType.Error:
      case eDatumType.Geforce:
      case eDatumType.LuxData:
      case eDatumType.Magnitude:
      case eDatumType.PPM:
      case eDatumType.Weight:
      case eDatumType.Speed:
      case eDatumType.WattHours:
      case eDatumType.Frequency:
      case eDatumType.Amps:
      case eDatumType.AmpHours:
      case eDatumType.UnscaledVoltage:
      case eDatumType.Voltage0To5:
      case eDatumType.Voltage0To10:
      case eDatumType.Voltage0To500:
      case eDatumType.Voltage0To1Point2:
      case eDatumType.Millimeter:
      case eDatumType.Micrograms:
      case eDatumType.Pascal:
      case eDatumType.CrestFactor:
      case eDatumType.Centimeter:
      case eDatumType.Voltage0To200:
      case eDatumType.Acceleration:
      case eDatumType.PPB:
      case eDatumType.MoistureWeight:
      case eDatumType.DifferentialPressureData:
      case eDatumType.MoistureTension:
      case eDatumType.AirSpeedData:
      case eDatumType.PPFDData:
      case eDatumType.PAR_DLI:
      case eDatumType.Decible:
      case eDatumType.Decibel:
        return d1.compvalue.ToDouble() >= d2.compvalue.ToDouble();
      case eDatumType.Count:
      case eDatumType.State:
        return d1.compvalue.ToInt() >= d2.compvalue.ToInt();
      default:
        throw new NotImplementedException();
    }
  }

  public static bool operator <(DataTypeBase d1, DataTypeBase d2)
  {
    if (d1.datatype != d2.datatype)
      throw new InvalidCastException();
    switch (d1.datatype)
    {
      case eDatumType.BooleanData:
      case eDatumType.Data:
      case eDatumType.Status:
      case eDatumType.DoorData:
      case eDatumType.WaterDetect:
      case eDatumType.VoltageDetect:
      case eDatumType.ActivityDetect:
      case eDatumType.ControlDetect:
      case eDatumType.ButtonPressed:
      case eDatumType.DryContact:
      case eDatumType.LightDetect:
      case eDatumType.MagnetDetect:
      case eDatumType.MotionDetect:
      case eDatumType.SeatOccupied:
      case eDatumType.AccelerometerImpact:
      case eDatumType.AirflowDetect:
      case eDatumType.VehicleDetect:
      case eDatumType.CarDetected:
      case eDatumType.ProbeStatus:
      case eDatumType.DeviceID:
      case eDatumType.MilliVoltsPerVolts:
        throw new InvalidOperationException();
      case eDatumType.Percentage:
      case eDatumType.TemperatureData:
      case eDatumType.Time:
      case eDatumType.Voltage:
      case eDatumType.ResistanceData:
      case eDatumType.Pressure:
      case eDatumType.Angle:
      case eDatumType.MilliAmps:
      case eDatumType.Coordinate:
      case eDatumType.Distance:
      case eDatumType.Error:
      case eDatumType.Geforce:
      case eDatumType.LuxData:
      case eDatumType.Magnitude:
      case eDatumType.PPM:
      case eDatumType.Weight:
      case eDatumType.Speed:
      case eDatumType.WattHours:
      case eDatumType.Frequency:
      case eDatumType.Amps:
      case eDatumType.AmpHours:
      case eDatumType.UnscaledVoltage:
      case eDatumType.Voltage0To5:
      case eDatumType.Voltage0To10:
      case eDatumType.Voltage0To500:
      case eDatumType.Voltage0To1Point2:
      case eDatumType.Millimeter:
      case eDatumType.Micrograms:
      case eDatumType.Pascal:
      case eDatumType.CrestFactor:
      case eDatumType.Centimeter:
      case eDatumType.Voltage0To200:
      case eDatumType.Acceleration:
      case eDatumType.PPB:
      case eDatumType.MoistureWeight:
      case eDatumType.DifferentialPressureData:
      case eDatumType.MoistureTension:
      case eDatumType.AirSpeedData:
      case eDatumType.PPFDData:
      case eDatumType.PAR_DLI:
      case eDatumType.Decible:
      case eDatumType.Decibel:
        return d1.compvalue.ToDouble() < d2.compvalue.ToDouble();
      case eDatumType.Count:
      case eDatumType.State:
        return d1.compvalue.ToInt() < d2.compvalue.ToInt();
      default:
        throw new NotImplementedException();
    }
  }

  public static bool operator <=(DataTypeBase d1, DataTypeBase d2)
  {
    if (d1.datatype != d2.datatype)
      throw new InvalidCastException();
    switch (d1.datatype)
    {
      case eDatumType.BooleanData:
      case eDatumType.Data:
      case eDatumType.Status:
      case eDatumType.DoorData:
      case eDatumType.WaterDetect:
      case eDatumType.VoltageDetect:
      case eDatumType.ActivityDetect:
      case eDatumType.ControlDetect:
      case eDatumType.ButtonPressed:
      case eDatumType.DryContact:
      case eDatumType.LightDetect:
      case eDatumType.MagnetDetect:
      case eDatumType.MotionDetect:
      case eDatumType.SeatOccupied:
      case eDatumType.AccelerometerImpact:
      case eDatumType.AirflowDetect:
      case eDatumType.VehicleDetect:
      case eDatumType.CarDetected:
      case eDatumType.ProbeStatus:
      case eDatumType.DeviceID:
      case eDatumType.MilliVoltsPerVolts:
        throw new InvalidOperationException();
      case eDatumType.Percentage:
      case eDatumType.TemperatureData:
      case eDatumType.Time:
      case eDatumType.Voltage:
      case eDatumType.ResistanceData:
      case eDatumType.Pressure:
      case eDatumType.Angle:
      case eDatumType.MilliAmps:
      case eDatumType.Coordinate:
      case eDatumType.Distance:
      case eDatumType.Error:
      case eDatumType.Geforce:
      case eDatumType.LuxData:
      case eDatumType.Magnitude:
      case eDatumType.PPM:
      case eDatumType.Weight:
      case eDatumType.Speed:
      case eDatumType.WattHours:
      case eDatumType.Frequency:
      case eDatumType.Amps:
      case eDatumType.AmpHours:
      case eDatumType.UnscaledVoltage:
      case eDatumType.Voltage0To5:
      case eDatumType.Voltage0To10:
      case eDatumType.Voltage0To500:
      case eDatumType.Voltage0To1Point2:
      case eDatumType.Millimeter:
      case eDatumType.Micrograms:
      case eDatumType.Pascal:
      case eDatumType.CrestFactor:
      case eDatumType.Centimeter:
      case eDatumType.Voltage0To200:
      case eDatumType.Acceleration:
      case eDatumType.PPB:
      case eDatumType.MoistureWeight:
      case eDatumType.DifferentialPressureData:
      case eDatumType.MoistureTension:
      case eDatumType.AirSpeedData:
      case eDatumType.PPFDData:
      case eDatumType.PAR_DLI:
      case eDatumType.Decible:
      case eDatumType.Decibel:
        return d1.compvalue.ToDouble() <= d2.compvalue.ToDouble();
      case eDatumType.Count:
      case eDatumType.State:
        return d1.compvalue.ToInt() <= d2.compvalue.ToInt();
      default:
        throw new NotImplementedException();
    }
  }

  public static DataTypeBase operator +(DataTypeBase d1, DataTypeBase d2)
  {
    if (d1.datatype != d2.datatype)
      throw new InvalidCastException();
    switch (d1.datatype)
    {
      case eDatumType.BooleanData:
      case eDatumType.State:
      case eDatumType.Status:
      case eDatumType.DoorData:
      case eDatumType.WaterDetect:
      case eDatumType.VoltageDetect:
      case eDatumType.ActivityDetect:
      case eDatumType.ControlDetect:
      case eDatumType.ButtonPressed:
      case eDatumType.DryContact:
      case eDatumType.LightDetect:
      case eDatumType.MagnetDetect:
      case eDatumType.MotionDetect:
      case eDatumType.SeatOccupied:
      case eDatumType.AccelerometerImpact:
      case eDatumType.AirflowDetect:
      case eDatumType.VehicleDetect:
      case eDatumType.UnscaledVoltage:
      case eDatumType.Voltage0To5:
      case eDatumType.Voltage0To10:
      case eDatumType.Voltage0To500:
      case eDatumType.Voltage0To1Point2:
      case eDatumType.Voltage0To200:
      case eDatumType.CarDetected:
      case eDatumType.ProbeStatus:
      case eDatumType.DeviceID:
        throw new Exception("Cannot add this type.");
      case eDatumType.Percentage:
        return (DataTypeBase) new Percentage(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.TemperatureData:
        return (DataTypeBase) new TemperatureData(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.Voltage:
        return (DataTypeBase) new Voltage(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.ResistanceData:
        return (DataTypeBase) new ResistanceData(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.Pressure:
        return (DataTypeBase) new Pressure(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.Angle:
        return (DataTypeBase) new Angle(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.MilliAmps:
        return (DataTypeBase) new MilliAmps(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.Coordinate:
        return (DataTypeBase) new Coordinate(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.Distance:
        return (DataTypeBase) new Distance(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.Error:
        return (DataTypeBase) new Error(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.Count:
        return (DataTypeBase) new Count(d1.compvalue.ToInt() + d2.compvalue.ToInt());
      case eDatumType.Geforce:
      case eDatumType.Acceleration:
        return (DataTypeBase) new Geforce(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.LuxData:
        return (DataTypeBase) new LuxData(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.Magnitude:
        return (DataTypeBase) new Magnitude(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.PPM:
        return (DataTypeBase) new PPM(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.Data:
        return (DataTypeBase) new Data((string) d1.compvalue + (string) d2.compvalue);
      case eDatumType.Weight:
        return (DataTypeBase) new Weight(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.Speed:
        return (DataTypeBase) new Speed(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.WattHours:
        return (DataTypeBase) new WattHours(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.Frequency:
        return (DataTypeBase) new Frequency(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.Amps:
        return (DataTypeBase) new Amps(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.AmpHours:
        return (DataTypeBase) new Amps(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.Millimeter:
        return (DataTypeBase) new Millimeter(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.Micrograms:
        return (DataTypeBase) new Micrograms(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.Pascal:
        return (DataTypeBase) new Pascal(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.CrestFactor:
        return (DataTypeBase) new CrestFactor(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.Centimeter:
        return (DataTypeBase) new Centimeter(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.PPB:
        return (DataTypeBase) new PPB(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.MoistureWeight:
      case eDatumType.MoistureTension:
        return (DataTypeBase) new MoistureWeight(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.DifferentialPressureData:
        return (DataTypeBase) new DifferentialPressureData(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.AirSpeedData:
        return (DataTypeBase) new AirSpeedData(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.PPFDData:
        return (DataTypeBase) new PPFDData(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.PAR_DLI:
        return (DataTypeBase) new PAR_DLI(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.Decible:
        return (DataTypeBase) new Decible(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.Decibel:
        return (DataTypeBase) new Decibel(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      case eDatumType.MilliVoltsPerVolts:
        return (DataTypeBase) new MilliVoltsPerVolts(d1.compvalue.ToDouble() + d2.compvalue.ToDouble());
      default:
        throw new NotImplementedException();
    }
  }

  public static DataTypeBase operator -(DataTypeBase d1, DataTypeBase d2)
  {
    if (d1.datatype != d2.datatype)
      throw new InvalidCastException();
    switch (d1.datatype)
    {
      case eDatumType.BooleanData:
      case eDatumType.State:
      case eDatumType.Data:
      case eDatumType.Status:
      case eDatumType.DoorData:
      case eDatumType.WaterDetect:
      case eDatumType.VoltageDetect:
      case eDatumType.ActivityDetect:
      case eDatumType.ControlDetect:
      case eDatumType.ButtonPressed:
      case eDatumType.DryContact:
      case eDatumType.LightDetect:
      case eDatumType.MagnetDetect:
      case eDatumType.MotionDetect:
      case eDatumType.SeatOccupied:
      case eDatumType.AccelerometerImpact:
      case eDatumType.AirflowDetect:
      case eDatumType.VehicleDetect:
      case eDatumType.UnscaledVoltage:
      case eDatumType.Voltage0To5:
      case eDatumType.Voltage0To10:
      case eDatumType.Voltage0To500:
      case eDatumType.Voltage0To1Point2:
      case eDatumType.Voltage0To200:
      case eDatumType.CarDetected:
      case eDatumType.ProbeStatus:
      case eDatumType.DeviceID:
        throw new Exception("Cannot Subtract this type.");
      case eDatumType.Percentage:
        return (DataTypeBase) new Percentage(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.TemperatureData:
        return (DataTypeBase) new TemperatureData(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.Voltage:
        return (DataTypeBase) new Voltage(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.ResistanceData:
        return (DataTypeBase) new ResistanceData(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.Pressure:
        return (DataTypeBase) new Pressure(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.Angle:
        return (DataTypeBase) new Angle(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.MilliAmps:
        return (DataTypeBase) new MilliAmps(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.Coordinate:
        return (DataTypeBase) new Coordinate(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.Distance:
        return (DataTypeBase) new Distance(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.Error:
        return (DataTypeBase) new Error(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.Count:
        return (DataTypeBase) new Count(d1.compvalue.ToInt() - d2.compvalue.ToInt());
      case eDatumType.Geforce:
      case eDatumType.Acceleration:
        return (DataTypeBase) new Geforce(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.LuxData:
        return (DataTypeBase) new LuxData(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.Magnitude:
        return (DataTypeBase) new Magnitude(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.PPM:
        return (DataTypeBase) new PPM(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.Weight:
        return (DataTypeBase) new Weight(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.Speed:
        return (DataTypeBase) new Speed(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.WattHours:
        return (DataTypeBase) new WattHours(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.Frequency:
        return (DataTypeBase) new Frequency(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.Amps:
        return (DataTypeBase) new Amps(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.AmpHours:
        return (DataTypeBase) new AmpHours(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.Millimeter:
        return (DataTypeBase) new Millimeter(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.Micrograms:
        return (DataTypeBase) new Micrograms(d1.compvalue.ToUInt() - d2.compvalue.ToUInt());
      case eDatumType.Pascal:
        return (DataTypeBase) new Pascal(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.CrestFactor:
        return (DataTypeBase) new CrestFactor(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.Centimeter:
        return (DataTypeBase) new Centimeter(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.PPB:
        return (DataTypeBase) new PPB(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.MoistureWeight:
      case eDatumType.MoistureTension:
        return (DataTypeBase) new MoistureWeight(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.DifferentialPressureData:
        return (DataTypeBase) new DifferentialPressureData(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.AirSpeedData:
        return (DataTypeBase) new AirSpeedData(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.PPFDData:
        return (DataTypeBase) new PPFDData(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.PAR_DLI:
        return (DataTypeBase) new PAR_DLI(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.Decible:
        return (DataTypeBase) new Decible(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.Decibel:
        return (DataTypeBase) new Decibel(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      case eDatumType.MilliVoltsPerVolts:
        return (DataTypeBase) new MilliVoltsPerVolts(d1.compvalue.ToDouble() - d2.compvalue.ToDouble());
      default:
        throw new NotImplementedException();
    }
  }

  public override int GetHashCode() => base.GetHashCode();

  public bool Equals(DataTypeBase other)
  {
    if (this.datatype != other.datatype)
      throw new InvalidCastException();
    switch (this.datatype)
    {
      case eDatumType.BooleanData:
      case eDatumType.DoorData:
      case eDatumType.WaterDetect:
      case eDatumType.VoltageDetect:
      case eDatumType.ActivityDetect:
      case eDatumType.ControlDetect:
      case eDatumType.ButtonPressed:
      case eDatumType.DryContact:
      case eDatumType.LightDetect:
      case eDatumType.MagnetDetect:
      case eDatumType.MotionDetect:
      case eDatumType.SeatOccupied:
      case eDatumType.AccelerometerImpact:
      case eDatumType.AirflowDetect:
        return this.compvalue.ToBool() == other.compvalue.ToBool();
      case eDatumType.Percentage:
      case eDatumType.TemperatureData:
      case eDatumType.Time:
      case eDatumType.Voltage:
      case eDatumType.ResistanceData:
      case eDatumType.Pressure:
      case eDatumType.Angle:
      case eDatumType.MilliAmps:
      case eDatumType.Coordinate:
      case eDatumType.Distance:
      case eDatumType.Error:
      case eDatumType.Geforce:
      case eDatumType.LuxData:
      case eDatumType.Magnitude:
      case eDatumType.PPM:
      case eDatumType.Weight:
      case eDatumType.Speed:
      case eDatumType.WattHours:
      case eDatumType.Frequency:
      case eDatumType.Amps:
      case eDatumType.AmpHours:
      case eDatumType.VehicleDetect:
      case eDatumType.UnscaledVoltage:
      case eDatumType.Voltage0To5:
      case eDatumType.Voltage0To10:
      case eDatumType.Voltage0To500:
      case eDatumType.Voltage0To1Point2:
      case eDatumType.Millimeter:
      case eDatumType.Micrograms:
      case eDatumType.Pascal:
      case eDatumType.CrestFactor:
      case eDatumType.Centimeter:
      case eDatumType.Voltage0To200:
      case eDatumType.Acceleration:
      case eDatumType.PPB:
      case eDatumType.CarDetected:
      case eDatumType.MoistureWeight:
      case eDatumType.DifferentialPressureData:
      case eDatumType.MoistureTension:
      case eDatumType.AirSpeedData:
      case eDatumType.PPFDData:
      case eDatumType.PAR_DLI:
      case eDatumType.Decible:
      case eDatumType.Decibel:
      case eDatumType.MilliVoltsPerVolts:
        return this.compvalue.ToDouble() == other.compvalue.ToDouble();
      case eDatumType.Count:
        return this.compvalue.ToInt() == other.compvalue.ToInt();
      case eDatumType.State:
      case eDatumType.Status:
        throw new InvalidOperationException();
      case eDatumType.Data:
      case eDatumType.ProbeStatus:
        return this.compvalue.ToStringSafe().Length > 1 && this.compvalue.ToStringSafe().Length > other.compvalue.ToStringSafe().Length && this.compvalue.ToStringSafe().ToLower().Contains(other.compvalue.ToStringSafe());
      case eDatumType.DeviceID:
        return this.compvalue.ToLong() == other.compvalue.ToLong();
      default:
        throw new NotImplementedException();
    }
  }

  public static void VerifyNotificationValues(Notification notification, string scale)
  {
    switch (notification.eDatumType)
    {
      case eDatumType.BooleanData:
        break;
      case eDatumType.Percentage:
        break;
      case eDatumType.TemperatureData:
        break;
      case eDatumType.Time:
        break;
      case eDatumType.Voltage:
        break;
      case eDatumType.ResistanceData:
        break;
      case eDatumType.Pressure:
        break;
      case eDatumType.Angle:
        break;
      case eDatumType.MilliAmps:
        break;
      case eDatumType.Coordinate:
        break;
      case eDatumType.Distance:
        break;
      case eDatumType.Error:
        break;
      case eDatumType.Count:
        break;
      case eDatumType.Geforce:
        break;
      case eDatumType.LuxData:
        break;
      case eDatumType.State:
        break;
      case eDatumType.Magnitude:
        break;
      case eDatumType.PPM:
        break;
      case eDatumType.Data:
        break;
      case eDatumType.Weight:
        break;
      case eDatumType.Speed:
        break;
      case eDatumType.Status:
        break;
      case eDatumType.WattHours:
        break;
      case eDatumType.Frequency:
        break;
      case eDatumType.DoorData:
        break;
      case eDatumType.WaterDetect:
        break;
      case eDatumType.VoltageDetect:
        break;
      case eDatumType.ActivityDetect:
        break;
      case eDatumType.ControlDetect:
        break;
      case eDatumType.ButtonPressed:
        break;
      case eDatumType.DryContact:
        break;
      case eDatumType.LightDetect:
        break;
      case eDatumType.MagnetDetect:
        break;
      case eDatumType.MotionDetect:
        break;
      case eDatumType.SeatOccupied:
        break;
      case eDatumType.AccelerometerImpact:
        break;
      case eDatumType.AirflowDetect:
        break;
      case eDatumType.Amps:
        break;
      case eDatumType.AmpHours:
        break;
      case eDatumType.VehicleDetect:
        break;
      case eDatumType.UnscaledVoltage:
        break;
      case eDatumType.Voltage0To5:
        break;
      case eDatumType.Voltage0To10:
        break;
      case eDatumType.Voltage0To500:
        break;
      case eDatumType.Voltage0To1Point2:
        break;
      case eDatumType.Millimeter:
        break;
      case eDatumType.Micrograms:
        break;
      case eDatumType.Pascal:
        break;
      case eDatumType.CrestFactor:
        break;
      case eDatumType.Centimeter:
        break;
      case eDatumType.Voltage0To200:
        break;
      case eDatumType.Acceleration:
        break;
      case eDatumType.PPB:
        break;
      case eDatumType.CarDetected:
        break;
      case eDatumType.MoistureWeight:
        break;
      case eDatumType.DifferentialPressureData:
        break;
      case eDatumType.MoistureTension:
        break;
      case eDatumType.ProbeStatus:
        break;
      case eDatumType.AirSpeedData:
        break;
      case eDatumType.PPFDData:
        break;
      case eDatumType.PAR_DLI:
        break;
      case eDatumType.Decible:
        break;
      case eDatumType.DeviceID:
        break;
      case eDatumType.Decibel:
        break;
      case eDatumType.MilliVoltsPerVolts:
        break;
      default:
        throw new NotImplementedException();
    }
  }
}
