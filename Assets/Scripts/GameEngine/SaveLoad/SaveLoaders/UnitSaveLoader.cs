using System;
using System.Linq;
using GameEngine.Data;
using GameEngine.Objects;
using GameEngine.Services;
using SaveSystem.Data;
using UnityEngine;
using Zenject;

namespace GameEngine.SaveLoad
{
    [Serializable]
    public class UnitsSaveLoader : SaveLoader<UnitService, UnitData[]>
    {
        [Inject] private UnitsPrefabs unitsPrefabs;

        override protected UnitData[] ConvertToData(UnitService service)
        {
            var units = service.GetAllUnits();

            var unitsArray = units.ToArray();

            UnitData[] unitDataArray = new UnitData[unitsArray.Length];

            for (int i = 0; i < unitsArray.Length; i++)
            {
                var unit = unitsArray[i];
                unitDataArray[i] = new UnitData
                {
                    name = unit.gameObject.name,
                    type = unit.Type,

                    hitPoints = unit.HitPoints,

                    xPosition = unit.gameObject.transform.position.x,
                    yPosition = unit.gameObject.transform.position.y,
                    zPosition = unit.gameObject.transform.position.z,

                    xRotation = unit.gameObject.transform.rotation.eulerAngles.x,
                    yRotation = unit.gameObject.transform.rotation.eulerAngles.y,
                    zRotation = unit.gameObject.transform.rotation.eulerAngles.z,
                };
            }

            return unitDataArray;
        }

        override protected void SetupData(UnitService service, UnitData[] dataArray)
        {
            var units = service.GetAllUnits();

            var enumerable = units as Unit[] ?? units.ToArray();

            foreach (var unitData in dataArray)
            {
                var position = new Vector3(unitData.xPosition, unitData.yPosition, unitData.zPosition);
                var rotation = new Vector3(unitData.xRotation, unitData.yRotation, unitData.zRotation);

                Unit unit = enumerable.FirstOrDefault(u => u != null && u.name == unitData.name);

                if (unit == null)
                {
                    SpawnSavedUnits(service, out unit, unitData, position, rotation);
                }

                unit.HitPoints = unitData.hitPoints;
                unit.gameObject.transform.position = position;
                unit.gameObject.transform.rotation = Quaternion.Euler(rotation);
            }

            service.SetupUnits(enumerable);

            DeleteUnsavedUnits(service, enumerable, dataArray);
        }

        private void SpawnSavedUnits(UnitService service, out Unit unit, UnitData unitData, Vector3 position,
            Vector3 rotation)
        {
            var unitPrefab = unitsPrefabs.Units.FirstOrDefault(prefab => prefab.name == unitData.type);

            var newUnit = service.SpawnUnit(unitPrefab, position, Quaternion.Euler(rotation));

            unit = newUnit;
        }

        private void DeleteUnsavedUnits(UnitService service, Unit[] enumerable, UnitData[] dataArray)
        {
            var difference = enumerable
                .Where(unit => dataArray.All(unitData => unitData.name != unit.name))
                .ToArray();

            foreach (var unit in difference)
            {
                service.DestroyUnit(unit);
            }
        }
    }
}