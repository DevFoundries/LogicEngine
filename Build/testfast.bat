@echo off
msbuild LogicEngine.xml /t:BuildCommon;AddEnableCoverage;TestOnly;SimianReport;GetCoverageReport